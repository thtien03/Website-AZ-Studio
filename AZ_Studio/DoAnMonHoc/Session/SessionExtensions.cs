using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace DoAnMonHoc.Session
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }
        public static T? Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
        public static async Task SetAsync<T>(this ISession session, string key, T value)
        {
            await Task.Run(() => session.Set(key, value));
        }
    }
}
/*
 sử dụng Session khi cần lưu thông tin trạng thái của người dùng qua các yêu cầu http liên tiếp
1.xác thực người dùng
2.giỏ hàng
    -session sd để lưu trữ thông tin giỏ hàng của người dùng,giúp giỏ hàng của người dùng được
giữ lại khi họ duyệt qua các sản phẩm và hoặc tắt và mở lại trình duyệt
 */
