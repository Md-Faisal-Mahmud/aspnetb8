using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototypePattern
{
	public class Document : ICloneable
	{
		public string Title { get; set; }
		public string FileName { get; set; }
		public string BodyText { get; set; }
		public Document Child { get; set; }

		public object Clone()
		{
			return Copy();
		}

		public Document Copy()
		{
			Document doc = new Document();
			doc.Child = new Document();
			doc.Child.Title = Child.Title;
			doc.Child.FileName = Child.FileName;
			doc.Child.BodyText = Child.BodyText;
			doc.Title = Title;
			doc.BodyText = BodyText;
			doc.FileName = FileName;

			return doc;
		}
	}
}
