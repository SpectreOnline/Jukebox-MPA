using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jukebox.Utilities 
{
    public class JukeboxHelper 
    {
        public static Session Session = new Session();
        public static List<int> SplitList(string songString)
        {

            List<string> songIds = songString.Split(',').ToList(); 

            songIds.ToString().Split(',').ToList();

            songIds = songIds.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();

            List<int> songsIdsInt = new List<int>();

            foreach (string id in songIds)
            {
                songsIdsInt.Add(Convert.ToInt32(id));
            }
            return songsIdsInt;
        }
    }
}
