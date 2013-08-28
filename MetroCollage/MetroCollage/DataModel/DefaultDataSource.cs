using MetroCollage.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

// The data model defined by this file serves as a representative example of a strongly-typed
// model.  The property names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs. If using this model, you might improve app 
// responsiveness by initiating the data loading task in the code behind for App.xaml when the app 
// is first launched.

namespace MetroCollage.DataModel
{
    /// <summary>
    /// Creates a collection of groups and items with content read from a static json file.
    /// 
    /// DefaultDataSource initializes with data read from a static json file included in the 
    /// project.  This provides sample data at both design-time and run-time.
    /// </summary>
    public sealed class DefaultDataSource
    {
        private static DefaultDataSource _defaultDataSource = new DefaultDataSource();

        private ObservableCollection<CanvasProject> _groups = new ObservableCollection<CanvasProject>();
        public ObservableCollection<CanvasProject> Groups
        {
            get { return this._groups; }
        }

        public static async Task<IEnumerable<CanvasProject>> GetGroupsAsync()
        {
            await _defaultDataSource.GetDefaultDataAsync();
            return _defaultDataSource.Groups;
        }

        public static async Task<CanvasProject> GetGroupAsync(int uniqueId)
        {
            await _defaultDataSource.GetDefaultDataAsync();
            // Simple linear search is acceptable for small data sets
            var matches = _defaultDataSource.Groups.Where((group) => group.Id.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static async Task<CanvasPicture> GetItemAsync(string uniqueId)
        {
            await _defaultDataSource.GetDefaultDataAsync();
            // Simple linear search is acceptable for small data sets
            IEnumerable<CanvasPicture> matches = _defaultDataSource.Groups.SelectMany(group => group.Pictures).Where((item) => item.Id.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        private async Task GetDefaultDataAsync()
        {
            if (this._groups.Count != 0)
                return;

            Uri dataUri = new Uri("ms-appx:///DataModel/DefaultData.json");

            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
            string jsonText = await FileIO.ReadTextAsync(file);
            JsonObject jsonObject = JsonObject.Parse(jsonText);
            JsonArray jsonArray = jsonObject["CanvasProjectsMRU"].GetArray();

            foreach (JsonValue groupValue in jsonArray)
            {
                JsonObject groupObject = groupValue.GetObject();
                CanvasProject group = new CanvasProject{ 
                    Id= Convert.ToInt32(groupObject["Id"].GetString()),
                    Name = groupObject["Name"].GetString(),
                    ImagePath = groupObject["ImagePath"].GetString(),
                    FilePath = groupObject["FilePath"].GetString()
                };
                group.Pictures = new List<CanvasPicture>();

                foreach (JsonValue itemValue in groupObject["Pictures"].GetArray())
                {
                    JsonObject itemObject = itemValue.GetObject();
                    group.Pictures.Add(new CanvasPicture
                    {
                        Id = Convert.ToInt32(itemObject["Id"].GetString()),
                        ImagePath = itemObject["ImagePath"].GetString(),
                        Left = itemObject["Left"].GetNumber(),
                        Top = itemObject["Top"].GetNumber(),
                        Rotation = itemObject["Rotation"].GetNumber(),
                        CanvasProjectId = group.Id
                    });
                }
                this.Groups.Add(group);
            }
        }
    }
}