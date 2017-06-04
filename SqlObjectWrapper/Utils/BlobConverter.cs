using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SqlObjectWrapper.Utils
{
    public static class BlobConverter<T>
    {
        public static T Decode(byte[] blob)
        {
            var binaryStream = new MemoryStream(blob);
            var formatter = new BinaryFormatter();
            var obj = (T)formatter.Deserialize(binaryStream);

            return obj;
        }

        public static byte[] Encode(T obj)
        {
            var binaryStream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(binaryStream,obj);

            var blob = binaryStream.ToArray();

            return blob;
        }
    }
}
