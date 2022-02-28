// This is the entry point for our whole application
const express = require("express");
const path = require('path');
const cors = require("cors")
const http = require('http')


const port = process.env.PORT || 8000;
const app = express();

//Cors-policy
app.use(cors({origin: "*",}))   // OR    //app.use(cors({origin: "http://127.0.0.1:8080",}))


//need to review
app.set('port', port)
const server = http.createServer(app)

//App use folder
app.use(express.static('app4'))

//Terminal Message
server.listen(port, () => console.log("server has started! port: " + port) );

//Main Page
//app.get("/", (req, res) => {res.send("Hello, here is your Unity WebGL game:");});

//App Page
app.get("/", (req, res) => {res.sendFile(path.join(__dirname, '/app4/index.html'));});


//Data Page
//app.get("/data/", (req, res) => {res.send("Hello, This is your Data Location");});



//App Page
//Old Location of the JSON file that was NOT made from a CSV file
app.get("/data/json/", (req, res) => {res.sendFile(path.join(__dirname, 'data1.json'));});



//App Page
//This is the location of the original CSV file
app.get("/data/csv/", (req, res) => {res.sendFile(path.join(__dirname, 'data.csv'));});



//App Page
//This one should be a JSON information which was made from a CSV file
app.get("/data/csv/json/", (req, res) => {res.sendFile(path.join(__dirname, 'data.csv'));});

