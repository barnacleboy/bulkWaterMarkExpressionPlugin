using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using PMlabs.GrfX.Framework.Expressions;

namespace XmlExpressionSample
{
    public class XmlExpression : Expression<XmlExpressionContext, string>
    {
        protected override string OnResolve()
        {
            var batchImage = this.Context.CurrentBatchImage; // Get info about the current image
            var batchImageFilename = batchImage.FileInfo.Name.ToLower();
            var xmlDocument = this.Context.Document; // Get xml from context
            
            // Reads all XmlElements of name "image" and...
            var description =
                xmlDocument.DocumentElement.ChildNodes.OfType<XmlElement>().Where(e => e.Name == "image" && 
                    e.Attributes.OfType<XmlAttribute>().Any(a => a.Name == "file" && a.Value.ToLower() == batchImageFilename))
                    .SingleOrDefault(); // ... return the value of the one element that equals our image filename

            if (description != null) return description.InnerText;
            return null;
        }
    }
}
