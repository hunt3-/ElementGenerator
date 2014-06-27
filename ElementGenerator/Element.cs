using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElementGenerator
{
    public class Element
    {

        private string _elementType;
        private List<Element> _children = new List<Element>();
        private List<ElementAttribute> _attributes = new List<ElementAttribute>();
        private string _value = "";

        public Element(string elementType)
        {
            _elementType = elementType;
        }

        public string tagName
        {
            get { return _elementType; }
            set { _elementType = value; }
        }

        public List<Element> children
        {
            get { return _children; }
            set { _children = value; }
        }

        public string value
        {
            get { return _value; }
            set { _value = value; }
        }

        public List<ElementAttribute> attributes
        {
            get { return _attributes; }
            set { _attributes = value; }
        }

        public void appendChild(Element element)
        {
            _children.Add(element);
        }

        public void appendChildren(List<Element> children)
        {
            foreach (Element _child in _children)
            {
                appendChild(_child);
            }
        }

        public void appendChildren(Element[] children)
        {
            for (int i = 0; i < children.Length; i++)
            {
                appendChild(children[i]);
            }
        }

        public void appendAttribute(ElementAttribute attribute)
        {
            _attributes.Add(attribute);
        }

        public void appendAttributes(List<ElementAttribute> attributes)
        {
            foreach (ElementAttribute attribute in attributes)
            {
                appendAttribute(attribute);
            }
        }

        public void appendAttributes(ElementAttribute[] attributes)
        {
            for (int i = 0; i < attributes.Length;i++ )
            {
                appendAttribute(attributes[i]);
            }
        }

        public string getString()
        {
            string returnString = "";
            returnString += "<" + _elementType;
            foreach (ElementAttribute attr in _attributes)
            {
                returnString += " " + attr.key + "='" + attr.value + "'";
            }
            if (_children.Count > 0 || _value.Length > 0)
            {
                returnString += ">";
                returnString += _value;
                foreach (Element child in _children)
                {
                    returnString += child.getString();
                }
                returnString += "</" + _elementType + ">";
            }
            else
            {
                returnString += " />";
            }
            return returnString;
        }

        public static List<Element> Parse(string toParse)
        {
            return new List<Element>();
        }

    }

    public class ElementAttribute
    {
       private string _key;
        private string _val;
        public ElementAttribute(string key, string val)
        {
            _key=key;
            _val = val;
        }

        public string key
        {
            get{return _key;}
            set{_key = value;}
        }

        public string value
        {
            get{return _val;}
            set{_val = value;}
        }

        public static List<ElementAttribute> Parse(string toParse)
        {
            List<ElementAttribute> returnList = new List<ElementAttribute>();
            string[] tmpArray=null;
            if (toParse.Contains(";"))
            {
                string[] tmparray = toParse.Split(';');
            }
            if (tmpArray.Length > 0)
            {
            for (int i = 0; i < tmpArray.Length; i++)
            {
                 if (tmpArray[i].Contains("="))
                 {
                    string[] attrArray = tmpArray[i].Split('=');
                    returnList.Add(new ElementAttribute(attrArray[0],attrArray[1]));
                  }
                }
            }
            else
            {
                if (toParse.Contains("="))
                {
                    string[] attrArray = toParse.Split('=');
                    returnList.Add(new ElementAttribute(attrArray[0], attrArray[1]));
                }
            }
            
            return returnList;
        }

    }

}
