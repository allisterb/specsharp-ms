using System.Text;
using System.Collections.Generic;
using System.IO;
using Microsoft.Contracts;

namespace Microsoft.Boogie
{

    [Immutable]
    public interface IToken
    {
        int kind { get; set; }         // token kind
        string filename { get; set; }  // token file
        int pos { get; set; }          // token position in the source text (starting at 0)
        int col { get; set; }          // token column (starting at 0)
        int line { get; set; }         // token line (starting at 1)
        string/*!*/ val { get; set; }  // token value

        bool IsValid { get; }
    }

    [Immutable]
    public class Token : IToken
    {
        public int _kind;    // token kind
        string _filename;  // token file
        public int _pos;     // token position in the source text (starting at 0)
        public int _col;     // token column (starting at 1)
        public int _line;    // token line (starting at 1)
        public string/*!*/ _val;  // token value
        public Token next;  // ML 2005-03-11 Tokens are kept in linked list

        public static IToken NoToken = new Token();

        public Token()
        {
            this._val = "anything so that it is nonnull";
        }
        public Token(int linenum, int colnum) :base()
        {
            this._line = linenum;
            this._col = colnum;
            this._val = "anything so that it is nonnull";
        }

        public int kind
        {
            get { return this._kind; }
            set { this._kind = value; }
        }

        public string filename
        {
            get { return this._filename; }
            set { this._filename = value; }
        }

        public int pos
        {
            get { return this._pos; }
            set { this._pos = value; }
        }

        public int col
        {
            get { return this._col; }
            set { this._col = value; }
        }

        public int line
        {
            get { return this._line; }
            set { this._line = value; }
        }

        public string/*!*/ val
        {
            get { return this._val; }
            set { this._val = value; }
        }

        public bool IsValid { get { return this._filename != null; } }


    }

    public static class ParserHelper
    {
        struct ReadState
        {
            public bool hasSeenElse;
            public bool mayStillIncludeAnotherAlternative;
            public ReadState(bool hasSeenElse, bool mayStillIncludeAnotherAlternative)
            {
                this.hasSeenElse = hasSeenElse;
                this.mayStillIncludeAnotherAlternative = mayStillIncludeAnotherAlternative;
            }
        }
        // "arg" is assumed to be trimmed
        private static bool IfdefConditionSaysToInclude(string arg, List<string> defines)
        {
            bool sense = true;
            while (arg.StartsWith("!"))
            {
                sense = !sense;
                arg = arg.Substring(1).TrimStart();
            }
            return defines.Contains(arg) == sense;
        }

        public static string Fill(FileStream fileStream, List<string> defines)
        {
            StreamReader reader = new StreamReader(fileStream);
            return Fill(reader, defines);
        }
        public static string Fill(TextReader reader, List<string> defines)
        {
            StringBuilder sb = new StringBuilder();
            List <ReadState> readState = new List<ReadState>();  // readState.Count is the current nesting level of #if's
            int ignoreCutoff = -1;  // -1 means we're not ignoring; for 0<=n, n means we're ignoring because of something at nesting level n
            while (true)
            //invariant -1 <= ignoreCutoff && ignoreCutoff < readState.Count;
            {
                string s = reader.ReadLine();
                if (s == null)
                {
                    if (readState.Count != 0)
                    {
                        sb.AppendLine("#MalformedInput: missing #endif");
                    }
                    break;
                }
                string t = s.Trim();
                if (t.StartsWith("#if"))
                {
                    ReadState rs = new ReadState(false, false);
                    if (ignoreCutoff != -1)
                    {
                        // we're already in a state of ignoring, so continue to ignore
                    }
                    else if (IfdefConditionSaysToInclude(t.Substring(3).TrimStart(), defines))
                    {
                        // include this branch
                    }
                    else
                    {
                        ignoreCutoff = readState.Count;  // start ignoring
                        rs.mayStillIncludeAnotherAlternative = true;  // allow some later "elsif" or "else" branch to be included
                    }
                    readState.Add(rs);
                    sb.AppendLine();  // ignore the #if line

                }
                else if (t.StartsWith("#elsif"))
                {
                    ReadState rs;
                    if (readState.Count == 0 || (rs = readState[readState.Count - 1]).hasSeenElse)
                    {
                        sb.AppendLine("#MalformedInput: misplaced #elsif");  // malformed input
                        break;
                    }
                    if (ignoreCutoff == -1)
                    {
                        // we had included the previous branch
                        //assert !rs.mayStillIncludeAnotherAlternative;
                        ignoreCutoff = readState.Count - 1;  // start ignoring
                    }
                    else if (rs.mayStillIncludeAnotherAlternative && IfdefConditionSaysToInclude(t.Substring(6).TrimStart(), defines))
                    {
                        // include this branch, but no subsequent branch at this level
                        ignoreCutoff = -1;
                        rs.mayStillIncludeAnotherAlternative = false;
                        readState[readState.Count - 1] = rs;
                    }
                    sb.AppendLine();  // ignore the #elsif line

                }
                else if (t == "#else")
                {
                    ReadState rs;
                    if (readState.Count == 0 || (rs = readState[readState.Count - 1]).hasSeenElse)
                    {
                        sb.AppendLine("#MalformedInput: misplaced #else");  // malformed input
                        break;
                    }
                    rs.hasSeenElse = true;
                    if (ignoreCutoff == -1)
                    {
                        // we had included the previous branch
                        //assert !rs.mayStillIncludeAnotherAlternative;
                        ignoreCutoff = readState.Count - 1;  // start ignoring
                    }
                    else if (rs.mayStillIncludeAnotherAlternative)
                    {
                        // include this branch
                        ignoreCutoff = -1;
                        rs.mayStillIncludeAnotherAlternative = false;
                    }
                    readState[readState.Count - 1] = rs;
                    sb.AppendLine();  // ignore the #else line

                }
                else if (t == "#endif")
                {
                    if (readState.Count == 0)
                    {
                        sb.AppendLine("#MalformedInput: misplaced #endif");  // malformed input
                        break;
                    }
                    readState.RemoveAt(readState.Count - 1);  // pop
                    if (ignoreCutoff == readState.Count)
                    {
                        // we had ignored the branch that ends here; so, now we start including again
                        ignoreCutoff = -1;
                    }
                    sb.AppendLine();  // ignore the #endif line

                }
                else if (ignoreCutoff == -1)
                {
                    sb.AppendLine(s);  // included line

                }
                else
                {
                    sb.AppendLine();  // ignore the line
                }
            }

            return sb.ToString();
        }
    }
}