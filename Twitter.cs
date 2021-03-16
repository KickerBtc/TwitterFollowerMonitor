using System;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using Tweetinvi;

namespace NoahnFollowers
{
    public class Twitter 
    {
        public static void MonitorFollowers() 
        {
            List<string> f1List = new List<string>();
            List<string> f2List = new List<string>();
        
            Auth.SetUserCredentials("CONSUMER KEY", "CONSUMER SECRET KEY", "USER ACCESS TOKEN", "USER SECRET ACCESS TOKEN");
            var user = User.GetAuthenticatedUser();
            Console.WriteLine(user);

            long noahID = 3215227305;
            var noah = User.GetUserFromId(noahID);

            //--- Gets first list of followers
            var followers1 = noah.GetFollowers(500);
            foreach (var follower in followers1) 
            {
                f1List.Add(follower.ScreenName);
            }

            Console.WriteLine(DateTime.Now + " - Watching for unfollowers (12 hours)...");
            Thread.Sleep(43200000);

            //--- Gets second list of followers
            var followers2 = noah.GetFollowers(258);
            foreach (var follower in followers2) 
            {
                f2List.Add(follower.ScreenName);
            }

            //--- This gets the names of unfollowers
            #region Unfollowers
            var unfollowersList = f1List.Except(f2List).ToList();
            string unfollowers = null;
            bool unfollowed = false;

            if (unfollowersList.Count > 1) 
            {
                foreach (var unfollower in unfollowersList) 
                {
                    unfollowers += "@" + unfollower + ", ";
                }
                unfollowed = true;
            }
            else if (unfollowersList.Count == 1) 
            {
                unfollowers = "@" + unfollowersList[0];
                unfollowed = true;
            }
            #endregion


            //--- This gets the names of new followers
            #region New Followers
            var newFollowersList = f2List.Except(f1List).ToList();
            string newFollowers = null;
            bool followed = false;

            if (newFollowersList.Count > 1) 
            {
                foreach (var newFollower in newFollowersList) 
                {
                    newFollowers += "@" + newFollower + ", ";
                }
                followed = true;
            }
            else if (newFollowersList.Count == 1)
            {
                newFollowers = newFollowersList[0];
                followed = true;
            }
            #endregion


            //--- Emails the results
            string text = null;
            string subject = null;
            if (unfollowed && followed) 
            {
                subject = "New Followers And Unfollowers";
                text = $"For every loss there is a new beginning. Welcome aboard {newFollowers} and farewell {unfollowers}";
            }
            else if (unfollowed) 
            {
                subject = "New Unfollower";
                text = $"Uff, looks like {unfollowers} unfollowed you. You're better off without em.";
            }
            else if (followed) 
            {
                subject = "New Follower!";
                text = $"Welcome aboard {newFollowers}!";
            }
            else 
            {
                subject = "All Quiet On The Twitter Front...";
                text = "No new followers or unfollowers today.";
            }

            Emailer.SendEmail(subject, text);
        }

    }
}
