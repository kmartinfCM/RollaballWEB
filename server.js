// Require Pagackes and data files
const express = require("express");
const path = require('path');
const cors = require("cors")
const http = require('http')
var XLSX = require('./node_modules/xlsx-style/xlsx.js');
var data = ReadFile('./data/data3.xlsx');
const appLocation = "app9"

var wwwhisper = require('connect-wwwhisper');
// app holds a reference to express or connect framework, it
// may be named differently in your source file.
app.use(wwwhisper());

// Alternatively, if you don't want wwwhisper to insert
// a logout iframe into HTML responses use.
app.use(wwwhisper(false));

const port = process.env.PORT || 8000;
const app = express();

//Cors-policy
//the "*" makes it for all ports to bi-pass CORS Policy
app.use(cors({origin: "*",}))   // OR    //app.use(cors({origin: "http://127.0.0.1:8080",}))

//need to review
app.set('port', port)
const server = http.createServer(app)

//App use folder
app.use(express.static(appLocation))

//Terminal Message
server.listen(port, () => console.log("server has started! port: " + port) );


//App Page
//app.get("/", (req, res) => {res.send("Hello, here is your Unity WebGL game:");});
app.get("/", (req, res) => {res.sendFile(path.join(__dirname, '/'+ appLocation +'/index.html'));});


//Data Page
//app.get("/data/", (req, res) => {res.send("Hello, This is your Data Location");});
app.get("/data/", (req, res) => {res.send(data);});





///FUNCTIONS


//Fuction to make XLSX into JSON
function ReadFile(filename)
{  
  const workbook = XLSX.readFile(filename);
  const name = workbook.SheetNames[0];
  const sheet = workbook.Sheets[name];
  return XLSX.utils.sheet_to_json(sheet);
}