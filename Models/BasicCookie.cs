using System.Collections.Generic;

namespace SignalRChat.Models
{
    public class BasicCookie
    {
        private User User;
        private List<User> UserList;
        private List<Chat> ChatList;
        private List<Message> MessageList;

        public BasicCookie(User user)
        {
            User = user;
            UserList = null;
            ChatList = null;
            MessageList = null;
        }

        public User GetUser()
        {
            return User;
        }

        // User list
        public List<User> GetUserList()
        {
            return UserList;
        }
        public void SetUserList(List<User> userList)
        {
            UserList = userList;
        }

        // Chat list
        public List<Chat> GetChatList()
        {
            return ChatList;
        }
        public void SetChatList(List<Chat> chatList)
        {
            ChatList = chatList;
        }

        // Message list
        public List<Message> GetMessageList()
        {
            return MessageList;
        }
        public void SetMessageList(List<Message> messageList)
        {
            MessageList = messageList;
        }

    }
}