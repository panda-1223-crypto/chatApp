namespace ChatApp.Entity
{
    public class ChatMessage
    {
        /// <summary>
        /// メッセージID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ユーザータイプ（ユーザ or Bot）
        /// </summary>
        public string UserMessage {  get; set; }
        /// <summary>
        /// メッセージ
        /// </summary>
        public string BotReply { get; set; }
        /// <summary>
        /// 登録日時
        /// </summary>
        public DateTime CreatedDate{ get; set; }

    }
}
