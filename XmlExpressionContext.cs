using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using PMlabs.GrfX;
using PMlabs.GrfX.Framework.Expressions;

namespace XmlExpressionSample
{
    public class XmlExpressionContext : ExpressionContext
    {
        private XmlDocument xmlDocument;

        public XmlExpressionContext() : base()
        {
            xmlDocument = new XmlDocument();
            var assemblyPath = Path.GetDirectoryName((new Uri(this.GetType().Assembly.CodeBase)).AbsolutePath);
            var xmlFilePath = Path.Combine(assemblyPath, "data.xml"); // Put your data.xml beneath the plug-in dll
            xmlDocument.Load(xmlFilePath); 
        }

        public XmlDocument Document { get { return this.xmlDocument; } }
    }
}
