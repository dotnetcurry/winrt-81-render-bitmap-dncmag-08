using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroCollage.DataModel
{
    public class CanvasDataSource : ICanvasDataSource
    {
        public async Task<bool> SaveCanvasProjectAsync(CanvasProject canvas)
        {
            bool returnValue = false;
            try
            {
                var objStorageHelper = new WinRtUtility.ObjectStorageHelper<CanvasProject>(WinRtUtility.StorageType.Local);
                await objStorageHelper.SaveAsync(canvas, canvas.Id.ToString());
                returnValue = true;
            }
            catch (Exception ex)
            {
                returnValue = false;
            }
            return returnValue;
        }

        public async Task<CanvasProject> LoadCanvasProjectAsync(int canvasId)
        {
            CanvasProject returnValue = null;
            try
            {
                var objStorageHelper = new WinRtUtility.ObjectStorageHelper<CanvasProject>(WinRtUtility.StorageType.Local);
                returnValue = await objStorageHelper.LoadAsync(canvasId.ToString());
            }
            catch (Exception ex)
            {
                returnValue = null;
            }
            return returnValue;
        }
    }
}
