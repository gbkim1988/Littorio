using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ReflectionTest
{
    public class MyObject
    {
        string _name;
        public string Name{
            get { return _name; }
            set { this._name = value; }
            }

    }
    class Program
    {
        static void Main(string[] args)
        {
            
            MyObject obj = new MyObject();
            PropertyInfo prop = obj.GetType().GetProperty("Name", BindingFlags.Public | BindingFlags.Instance);
            if (null != prop && prop.CanWrite)
            {
                prop.SetValue(obj, "MyName", null);
            }

            Console.WriteLine(obj.Name);
        }
    }
}
