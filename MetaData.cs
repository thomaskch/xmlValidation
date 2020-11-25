using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ValidatorApp
{
    class MetaData
    {
        private string metaDataXmlFileLocation;
        private XmlDocument xmlDocument;
        private string tableToValidateLocation;

        public MetaData(string metaDataXmlFileLocation, string tableFileLocation)
        {
            tableToValidateLocation = tableFileLocation;
            xmlDocument = new XmlDocument();
            xmlDocument.Load(metaDataXmlFileLocation);
        }

        public string MetaDataFileLocation { get => metaDataXmlFileLocation; set => metaDataXmlFileLocation = value; }
        public XmlDocument XmlDocument { get => xmlDocument; set => xmlDocument = value; }
        public string TableToValidateLocation { get => tableToValidateLocation; set => tableToValidateLocation = value; }

        public string validateColumns()
        {


            XmlElement root = xmlDocument.DocumentElement;


           var tableNodes =  FindNode(root.ChildNodes, "tables");

           foreach(XmlElement tableNode in tableNodes)
            {
             var column = FindNode(tableNode.ChildNodes, "column");

                string type = column["type"].InnerText;

            }

            return "be om";
        }


        private XmlNode FindNode(XmlNodeList list, string nodeName)
        {
            if (list.Count > 0)
            {
                foreach (XmlNode node in list)
                {
                    if (node.Name.Equals(nodeName))
                    {
                        return node;
                    }
                    if (node.HasChildNodes)
                    {
                        XmlNode nodeFound = FindNode(node.ChildNodes, nodeName);
                        if (nodeFound != null)
                            return nodeFound;
                    }
                }
            }
            return null;
        }
    }
}
