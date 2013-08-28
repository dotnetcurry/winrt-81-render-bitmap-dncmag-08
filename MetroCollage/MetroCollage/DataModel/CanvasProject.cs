using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroCollage.DataModel
{
    public class CanvasProject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public string ImagePath { get; set; }
        public List<CanvasPicture> Pictures  { get; set; }
    }
}
