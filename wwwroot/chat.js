const chat = document.getElementById("chat");

document.getElementById("send").addEventListener("click", async () => {
    const msg = document.getElementById("msg").value;
    if(!msg){
        console.log("入力値空白のため処理中断。")
        return;
    }

    // --- ユーザーのメッセージを表示 ---
    chat.innerHTML += `<div class = "user"> あなた: ${msg}</div>`;
    setLocalStorage({ sender: "user", text: msg });
    document.getElementById("msg").value ="";

    // --- サーバーへ送信 ---
    const res = await fetch("/api/chat", {
        method : "POST",
        headers : {"Content-Type" : "application/json"},
        body : JSON.stringify({message: msg})
    })

    if (res.status == 404) {
        console.log("通信失敗");
        alert("通信失敗。システムにご連絡をお願いいたします。");

        return;
    }

    // --- ボットの返信を表示 ---
    const data = await res.json();
    chat.innerHTML += `<div class ="bot"> ボット: ${data.reply} </div>`;
    chat.scrollTop = chat.scrollHeight;
    setLocalStorage({sender: "bot", text: data.reply});

})

window.addEventListener("DOMContentLoaded", () => {
    const history = getLocalStorage();
    updateChat(history);

})

// =======================
// LocalStorage 関連関数
// =======================

function setLocalStorage(chatMessage) {
    const history = getLocalStorage();
    history.push(chatMessage);
    localStorage.setItem("history", JSON.stringify(history));
}

function getLocalStorage() {
    const history = JSON.parse(localStorage.getItem("history")) || [];
    return history;
}

function updateChat(chatHistory) {
    chat.innerHTML = "";

    chatHistory.forEach(item => {
        if (item.sender == "user") {
            chat.innerHTML += `<div class="user"> あなた: ${item.text} </div>`;
        } else {
            chat.innerHTML += `<div class="bot"> ボット: ${item.text} </div>`;
        }
    })

    chat.scrollTop = chat.scrollHeight;
}

document.getElementById("msg").addEventListener("keydown", function (event) {
    if (event.key === "Enter" && !event.shiftKey) {
        event.preventDefault(); // 改行を防ぐ
        document.getElementById("send").click(); // 送信ボタンをクリックしたのと同じ動作
    }
});

document.getElementById("clearHistory").addEventListener("click", function () {
    if (window.confirm("削除してもよろしいですか?")) {
        localStorage.removeItem("history");
        chat.innerHTML = "";
        alert("履歴を削除しました");
    } else {
        return;
    }

})