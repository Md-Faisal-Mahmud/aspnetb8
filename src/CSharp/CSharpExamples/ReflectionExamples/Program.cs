using System.Reflection;
using System.IO;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using Newtonsoft.Json;

DirectoryInfo directory = new DirectoryInfo(Directory.GetCurrentDirectory())
    .Parent.Parent.Parent;

string config = File.ReadAllText(directory.GetFiles()
    .Where(x => x.Name.Contains("config")).First().FullName);

dynamic configJson = JsonConvert.DeserializeObject(config);

Type t = Assembly.LoadFile(directory.Parent.GetFiles()
    .Where(x => x.Extension == ".dll").First().FullName).GetTypes()
    .Where(x => x.Name == configJson.ClassName.ToString()
    && x.GetInterface("IPlugin") != null).First();

ConstructorInfo constructor = t?.GetConstructor(new Type[] { typeof(string) });

object o = constructor?.Invoke(new object[] { "Demo Report" });

MethodInfo method = t?.GetMethod("Start", BindingFlags.NonPublic | BindingFlags.Instance, new Type[] { });

object r = method?.Invoke(o, new object[] { });
/*
PropertyInfo propertyInfo = null;
if(propertyInfo.PropertyType == typeof(Guid))
{

}
*/


