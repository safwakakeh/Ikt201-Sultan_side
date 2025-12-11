(function () {
    // Liste over ord du vil blokkere (kan utvides)
    const bannedWords = [
        // Norsk banning
        "faen", "fanden", "jævla", "jævlig", "dritt", "helvete",
        "kuk", "fitte", "pikk", "hore", "idiot", "mongo",

        // Engelsk banning
        "fuck", "shit", "bitch", "asshole", "bastard",

        // Forkledde varianter
        "f*ck", "f**k", "sh1t", "b1tch", "f@en",

        // Hatefulle og rasistiske mønstre (GENERALE BLOKKERINGER)
        "nigger", "neger", "jew", "muslimjævel", "svarting", "pakkis",

        // Voldelige ytringer
        "drep", "kill", "henrett", "skyt"
    ];


    function containsBadWords(text) {
        const lower = text.toLowerCase();
        return bannedWords.some((w) => lower.includes(w));
    }

    function addMessage(role, text) {
        const container = document.getElementById("sultan-ai-messages");
        if (!container) return;

        const wrapper = document.createElement("div");
        wrapper.style.marginBottom = "6px";
        wrapper.style.textAlign = role === "user" ? "right" : "left";

        const bubble = document.createElement("span");
        bubble.style.display = "inline-block";
        bubble.style.padding = "8px 10px";
        bubble.style.borderRadius = "12px";
        bubble.style.maxWidth = "85%";
        bubble.style.whiteSpace = "pre-wrap";
        bubble.style.fontSize = "16px";

        if (role === "user") {
            bubble.style.background = "#708090"; // Slate Grey
            bubble.style.color = "white";
        } else {
            bubble.style.background = "#e5e7eb"; // Light grayish blue
            bubble.style.color = "#111827";
        }

        bubble.textContent = text;
        wrapper.appendChild(bubble);
        container.appendChild(wrapper);
        container.scrollTop = container.scrollHeight;
    }

    async function sendMessage(text) {
        if (containsBadWords(text)) {
            addMessage(
                "model",
                "Vi ønsker å holde chatten hyggelig. Vennligst unngå støtende språk."
            );
            return;
        }

        addMessage("user", text);

        try {
            const response = await fetch("/api/chat", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({ message: text }),
            });

            if (!response.ok) {
                console.log("Chat API-feil:", response.status, response.statusText);
                addMessage(
                    "model",
                    "Beklager, noe gikk galt med serveren. Prøv igjen senere."
                );
                return;
            }

            const data = await response.json();
            addMessage("model", data.reply || "Jeg fant ikke noe svar akkurat nå.");
        } catch (err) {
            console.error(err);
            addMessage(
                "model",
                "Jeg klarte ikke å koble til serveren. Sjekk nettet og prøv igjen."
            );
        }
    }

    function init() {
        // Chat-knapp
        const button = document.createElement("button");
        button.id = "sultan-ai-toggle";
        button.textContent = "💬 Chat med Sultan";
        Object.assign(button.style, {
            position: "fixed",
            bottom: "20px",
            right: "20px",
            padding: "10px 16px",
            borderRadius: "999px",
            border: "none",
            background: "#154F2E",
            color: "white",
            cursor: "pointer",
            fontSize: "14px",
            zIndex: 9999,
            boxShadow: "0 4px 10px rgba(0,0,0,0.25)",
        });

        // 📦 Selve chat-boksen
        const box = document.createElement("div");
        box.id = "sultan-ai-box";
        Object.assign(box.style, {
            position: "fixed",
            bottom: "80px",
            right: "20px",
            width: "420px",
            height: "600px",
            background: "white",
            borderRadius: "12px",
            border: "1px solid #e5e7eb",
            display: "none",
            flexDirection: "column",
            overflow: "hidden",
            zIndex: 9999,
            boxShadow: "0 8px 20px rgba(0,0,0,0.3)",
            fontFamily: "system-ui, -apple-system, BlinkMacSystemFont, sans-serif",
            fontSize: "14px",
        });

        box.innerHTML = `
      <div style="padding:8px 10px; background:#f9fafb; border-bottom:1px solid #e5e7eb; font-weight:bold; font-size:15px;">
        Sultan Oslo Food & Sweets – AI
      </div>
      <div id="sultan-ai-messages" style="flex:1; padding:10px; overflow-y:auto; background:#f3f4f6; font-size:16px;"></div>
      <div style="padding:8px; border-top:1px solid #e5e7eb;">
        <input id="sultan-ai-input" type="text" placeholder="Skriv en melding..." style="width:100%; padding:8px; font-size:16px; border:1px solid #d1d5db; border-radius:6px;" />
      </div>
    `;

        document.body.appendChild(button);
        document.body.appendChild(box);

        const input = document.getElementById("sultan-ai-input");

        // 🎉 Velkomstmelding
        addMessage(
            "model",
            "Velkommen til Sultan Oslo Food & Sweets! Spør meg om meny, søtsaker eller åpningstider. أهلاً وسهلاً!"
        );

        let isOpen = false;

        button.addEventListener("click", () => {
            isOpen = !isOpen;
            box.style.display = isOpen ? "flex" : "none";
        });

        input.addEventListener("keydown", (e) => {
            if (e.key === "Enter") {
                const text = input.value.trim();
                if (!text) return;
                input.value = "";
                sendMessage(text);
            }
        });
    }

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", init);
    } else {
        init();
    }
})();