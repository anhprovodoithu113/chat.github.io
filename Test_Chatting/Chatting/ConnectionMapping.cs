using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test_Chatting.Chatting
{
    public class ConnectionMapping<T>
    {
        private readonly Dictionary<T, HashSet<string>> _connections = new Dictionary<T, HashSet<string>>();
        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }
        public void Add(T key, string connectionId)
        {
            // Khi sử dụng SignalR, sẽ có nhiều tiểu trình (thread) được mở song song 
            // như là sử dụng nhiều thiết bị để đăng nhập cùng một account hoặc nhiều 
            // account đăng nhập vào cùng lúc. Đó chính là lúc chúng ta dùng lock để khóa
            // cái thread hiện tại của mỗi lần đăng nhập và kiểm tra nó
            lock (_connections)
            {
                HashSet<string> connections;
                // Kiểm tra connection mới đã có trong Dictionary hiện tại hay chưa
                if(!_connections.TryGetValue(key, out connections))
                {
                    // Nếu chưa có, tạo connection này thành kiểu HashSet mới
                    // và thêm vào danh sách connection trong Dictionary hiện tại
                    connections = new HashSet<string>();
                    _connections.Add(key, connections);
                }

                lock (connections)
                {
                    connections.Add(connectionId);
                }
            }
        }

        public IEnumerable<string> GetConnections(T key)
        {
            HashSet<string> connections;
            if(_connections.TryGetValue(key, out connections))
            {
                return connections;
            }
            return Enumerable.Empty<string>();
        }

        public void Remove(T key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if(!_connections.TryGetValue(key, out connections))
                {
                    return;
                }
                lock (connections)
                {
                    connections.Remove(connectionId);

                    if(connections.Count == 0)
                    {
                        _connections.Remove(key);
                    }
                }
            }
        }
    }
}