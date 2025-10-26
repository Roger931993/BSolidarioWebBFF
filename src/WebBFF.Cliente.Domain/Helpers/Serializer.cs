﻿using System.Xml.Serialization;

namespace WebBFF.Cliente.Domain.Helpers
{
    public static class Serializer
    {
        public static string SerializeToXml<T>(T obj)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var stringWriter = new StringWriter())
            {
                serializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }
    }
}
