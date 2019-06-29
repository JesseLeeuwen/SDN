using System.Collections.Generic;
using SDN.Utilities;

namespace SDN
{
    public class Token
    {
        public string Name;
        public List<string> Values;
        // simple get / set for first element
        public string Value{
            get{ return Values[0]; }
            set{ 
                if( Values.Count == 0 )
                    Values.Add(string.Empty);

                Values[0] = value; 
            }
        }
        public List<Token> Children;

        // create blank token
        public Token()
        {
            Name = "None";
            Children = new List<Token>();
            Values = new List<string>();
        }
        
        // create token with data fro another token
        public Token(Token p_other)
        {
            Name = p_other.Name;
            Values = p_other.Values;
            Children = p_other.Children;
        }

        // Get Value as specific Type 
        public T GetValue<T>()
        {
            return Utils.ConvertValue<T>(Value);
        }

        // add child to the children list
        public void AddChild( Token p_child )
        {
            Children.Add( p_child );
        }

        // search for token with "name" in children list
        public Token FindChild(string p_name)
        {
            for( int i = 0; i < Children.Count; ++i)
            {
                if( Children[i].Name == p_name )
                    return Children[i];
            }
            return null;
        }

        // generate string for token class
        public override string ToString()
        {
            string valueString = string.Join(",", Values);
            string childString = string.Join(" ", Children);
            return string.Format("{0}:[{1}{2}]", Name, valueString, childString);
        }
    }
}
