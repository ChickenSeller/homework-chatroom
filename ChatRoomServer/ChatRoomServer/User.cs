using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Net;


namespace ChatRoomServer
{
    class User
    {
        public ArrayList users;

        public User()
        {
            this.users = new ArrayList();
        }
        public UserNode AddUser(string UserIP, int UserPort, string UserName)
        {
            UserNode newUser = new UserNode(UserIP, UserPort, UserName);
            this.users.Add(newUser);
            return newUser;
        }
        public UserNode GetUserByID(int id)
        {
            foreach(UserNode tempUser in this.users)
            {
                if (tempUser.UserID == id)
                {
                    return tempUser;
                }
            }
            return null;
        }

    }

    class UserNode
    {
        public string UserName;
        public int UserID;
        public string UserIP;
        public int UserPort;
        public static int UserNum;
        public UserNode(string UserIP,int UserPort,string UserName)
        {
            this.UserName = UserName;
            UserNode.UserNum += 1;
            this.UserID = UserNode.UserNum;
            this.UserIP = UserIP;
            this.UserPort = UserPort;
        }
    }
}
