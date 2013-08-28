using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Windows.Storage;
using Windows.UI.Xaml.Media;

namespace MetroCollage.DataModel
{
    public class CanvasPicture
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public double Top { get; set; }
        public double Left { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Rotation { get; set; }
        public int CanvasProjectId { get; set; }
        [XmlIgnore]
        public StorageFile SourceFile { get; set; }
    }
}
