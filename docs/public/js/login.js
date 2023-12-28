var currentUpdate = 0;
function clearConsole(){
    document.getElementById("console").innerHTML = "";
}
function logConsoleText(text, color){
    var line = document.createElement("p");
    line.setAttribute("class", "align-left analog-font text-lg text-" + color);
    line.innerHTML = text;
    document.getElementById("console").appendChild(line);
}
function logConsoleBreak(){
    var line = document.createElement("br");
    document.getElementById("console").appendChild(line);
}

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }
  
var socket;
function openConnection(){
    return new Promise((resolve) => {
        if (localStorage.getItem("address") != null && localStorage.getItem("key") != null){
            const address = "ws://" + localStorage.getItem("address") + "/websocket/";
            socket = new WebSocket(address);
            var valid = false;
            // Connection opened
            socket.onopen = function(event) {
                socket.send(localStorage.getItem("key"));
            };
    
            
            // Listen for messages
            socket.onmessage = function(event) {
                if (event.data == "authenticated"){
                    valid = true;
                    socket.close();
                }
            };
    
            socket.onclose = function(event) {
                if (valid){
                    resolve(true);
                }else{
                    resolve(false);
                }
            };
    
            sleep(5000).then(() => { socket.close(); });
        }
        
    });
    
}

function update(){
    currentUpdate++;
    clearConsole();
    const thisUpdate = currentUpdate;
    document.getElementById("confirm").setAttribute("class", "hidden");
    localStorage.setItem("address", document.getElementById("address").value);
    localStorage.setItem("key", document.getElementById("key").value);
    const address = "ws://" + localStorage.getItem("address") + "/websocket/";
    logConsoleText("Attempt connect to " + localStorage.getItem("address"), "white");
    logConsoleBreak();
    // Create WebSocket connection.
    const updateSocket = new WebSocket(address);

    
    var valid = false;
    // Connection opened
    updateSocket.onopen = function(event) {
        if (currentUpdate == thisUpdate){
            logConsoleText("Connected to websocket", "emerald-500");
            updateSocket.send(localStorage.getItem("key"));
            logConsoleText("Sending Auth", "gray-500");
        }
    };


    updateSocket.onclose = function(event) {
        if (currentUpdate == thisUpdate){
            logConsoleBreak();
            if (!valid){
                logConsoleText("Disconnected...", "red-500");
            }else{
                logConsoleText("Connected...", "emerald-500");
                logConsoleText("Ready to Confirm...", "white");
                document.getElementById("confirm").setAttribute("class", "");
            }
        }
    };

    // Listen for messages
    updateSocket.onmessage = function(event) {
        if (currentUpdate == thisUpdate){
            if (event.data == "authenticated"){
                logConsoleText("Websocket validated key", "gray-500");
                valid = true;
                updateSocket.close();
            }
        }
    };

    sleep(5000).then(() => { updateSocket.close(); });

}

function startSetup(){
    clearConsole();
    logConsoleText("Loading Default Values...", "yellow");
    const address = localStorage.getItem("address");
    const key = localStorage.getItem("key");
    if (address == null){
        address = "localhost:8080";
    }
    if (key == null){
        key = "default";
    }
    document.getElementById("address").value = address;
    document.getElementById("key").value = key;
    update();
}
