using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using MongoDB.Driver;
using System.Configuration;

namespace VendaERP.Core
{
    public static class ExtensionMethods
    {
        public static T CopyMongoEntitiy<T>(this T OriginalEntity, T NewEntity)
        {
            PropertyInfo[] oProperties = OriginalEntity.GetType().GetProperties();

            foreach (PropertyInfo CurrentProperty in oProperties.Where(p => p.CanWrite))
                if (CurrentProperty.GetValue(NewEntity, null) != null)
                    CurrentProperty.SetValue(OriginalEntity, CurrentProperty.GetValue(NewEntity, null), null);

            return OriginalEntity;
        }
    }
}