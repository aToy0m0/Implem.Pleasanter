using Newtonsoft.Json.Linq;
using System;
using System.Linq;
namespace Implem.Pleasanter.Libraries.Settings
{
    public static class ServerScriptSettingsUtilities
    {
        public static bool HasDefinition(SiteSettings ss)
        {
            return ss?.ServerScripts?.Any() == true
                || ss?.ServerScriptsAllDisabled == true
                || ss?.ServerScriptsGetErrorDetails == true;
        }

        public static bool SameDefinition(SiteSettings left, SiteSettings right)
        {
            return JToken.DeepEquals(
                JToken.FromObject(left?.ServerScripts?.Select(script => script.GetRecordingData()).ToArray()
                    ?? Array.Empty<ServerScript>()),
                JToken.FromObject(right?.ServerScripts?.Select(script => script.GetRecordingData()).ToArray()
                    ?? Array.Empty<ServerScript>()))
                && (left?.ServerScriptsAllDisabled == true) == (right?.ServerScriptsAllDisabled == true)
                && (left?.ServerScriptsGetErrorDetails == true) == (right?.ServerScriptsGetErrorDetails == true);
        }

        public static void PreserveDefinition(SiteSettings target, SiteSettings current)
        {
            target.ServerScripts = current.ServerScripts;
            target.ServerScriptsAllDisabled = current.ServerScriptsAllDisabled;
            target.ServerScriptsGetErrorDetails = current.ServerScriptsGetErrorDetails;
        }

        public static void PreserveOmittedDefinition(SiteSettings target, SiteSettings current)
        {
            target.ServerScripts = target.ServerScripts ?? current?.ServerScripts;
            target.ServerScriptsAllDisabled = target.ServerScriptsAllDisabled ?? current?.ServerScriptsAllDisabled;
            target.ServerScriptsGetErrorDetails = target.ServerScriptsGetErrorDetails ?? current?.ServerScriptsGetErrorDetails;
        }

        public static void RemoveDefinition(SiteSettings ss)
        {
            ss.ServerScripts = null;
            ss.ServerScriptsAllDisabled = null;
            ss.ServerScriptsGetErrorDetails = null;
        }
    }
}