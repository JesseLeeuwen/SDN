using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace SDN
{
    public class Parser
    {
        public Token rootToken;
        
        // regexes used by sdn
        private static Regex tokenSelection     = new Regex( @"(?:([^\s\[\]]*):\s?|\[([^\[]*?)\]|\])" );
        private static Regex commentSelection   = new Regex( @"(\#[^\n]*?)" );
        private static Regex pathSplitting      = new Regex( @"(?:([a-zA-Z]+)|\[([0-9]{1,})\])" );
        private static Regex contentSpltting    = new Regex( @"(?=\S)(?:("")([^""]+)("")|([^""\[\],]+))" );
        
        public Parser(string p_sdnSource)
        {
            // create root node
            rootToken = new Token(){Name="root", Value="none"};

            // parse source 
            // get all comment lines in source and delete the comments from source
            MatchCollection commentMatches = commentSelection.Matches(p_sdnSource);
            foreach( Match commentMatch in commentMatches)
                p_sdnSource = p_sdnSource.Remove(commentMatch.Index, commentMatch.Length);
            
            // create a parent stack
            Stack<Token> hierachyStack = new Stack<Token>();
            hierachyStack.Push( rootToken );

            // search for sdn tokens
            Match match = tokenSelection.Match(p_sdnSource);
            while(match.Success)
            {                           
                // Get token type
                int groupIndex = 1;
                while( match.Groups[groupIndex].Success == false && groupIndex < 3 )
                    groupIndex++;

                switch(groupIndex)
                {
                case 1:
                    // new token
                    Token newToken = new Token(){ Name = CleanPropertyName( match.Captures[0].Value) };
                    hierachyStack.Peek().AddChild( newToken );
                    hierachyStack.Push( newToken );                    
                    break;
                case 2:
                    // add content to current token
                    Match m = contentSpltting.Match( match.Captures[0].Value );
                    while(m.Success)
                    {
                        // get value
                        string valueToAdd = m.Groups[4].Value;
                        
                        // upadte value to the string value
                        if( m.Groups[1].Success && m.Groups[3].Success)
                            valueToAdd = m.Groups[2].Value;
                        
                        // add to value to value list
                        hierachyStack.Peek().Values.Add(valueToAdd);
                        m = m.NextMatch();
                    }   
                    // end of content
                    hierachyStack.Pop();
                    break;                 
                case 3:
                    // closing bracket(])
                    hierachyStack.Pop();
                    break; 
                default:
                    break;
                }

                match = match.NextMatch();
            }
        }

        //get node by path / name
        public Token GetToken(string p_path)
        {
            // first node to search
            Token result = rootToken;

            // loop through path segments
            Match match = pathSplitting.Match(p_path);
            
            // check for valid start path
            if( match.Success == false )
                return null;

            while( match.Success )
            {
                // Get value at index
                if( match.Groups[2].Success  )
                {
                    int index = int.Parse( match.Groups[2].Value );
                    // create temp token for array value
                    return new Token(){
                        Name=string.Format("{0}[{1}]", result.Name, index),
                        Value = result.Values[index]
                    };
                }
                
                // search for child node
                result = result.FindChild( match.Groups[1].Value );
                
                // check if token was found( valid path )
                if( result == null )
                    return null;
                
                match = match.NextMatch();
            }

            return result;
        }

        //node exists
        public bool TokenExists(string p_path)
        {
            return GetToken(p_path) != null;
        }

        //get node value
        public T GetValue<T>(string p_path)
        {
            Token token = GetToken(p_path);

            if( token == null )
                return default(T);

            return token.GetValue<T>();
        }

        // write SDN string to file
        public void WriteTo(string p_path)
        {
            FileStream fs = new FileStream( p_path, FileMode.Create );
            WriteTo( fs );
        }

        // write SDN string to stream
        public void WriteTo(Stream p_stream)
        {
            byte[] bytesToWrite = Encoding.ASCII.GetBytes( this.ToString() );
            p_stream.Write( bytesToWrite, 0, bytesToWrite.Length );
        }
        
        // override ToString methods, all child of the root node are added as string to result
        public override string ToString()
        {
            string result = string.Empty;
            foreach(Token t in rootToken.Children)
                result += t.ToString();
            return result;
        }

        // remove spaces, semicolons and opening bracket from string
        // used to clean property values
        private string CleanPropertyName(string p_name)
        {       
            return p_name
                .Replace(":", "")
                .Trim();
        }
    }
}