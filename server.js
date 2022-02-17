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
app.get("/data/main/", (req, res) => {res.sendFile(path.join(__dirname, 'data1.json'));});



///DATA
app.get("/data/default", (req, res) => {
    var dummyData = {
        userid: "default",
        username: "defaultUser",
        wins: 18,
        losses: 1000,
        color: 
            [
            { red:    .1 },
            { green:  .6 },
            { blue:   .7 },
            { alpha:  1 },
            ],
        someArray: 
            [
            { name: "foo", value: 0.25 },
            { name: "bar", value: 0.5 },
            { name: "baz", value: 0.75 },
            ]
    };
    res.json(dummyData);
});

app.get("/data/kmartinf", (req, res) => {
    var dummyData = {
        userid: "kmartinf",
        username: "Martin",
        wins: 20,
        losses: 40,
        color: 
            [
            { red:    .1 },
            { green:  .7 },
            { blue:   .6 },
            { alpha:  1 },
            ],
        someArray: 
            [
            { name: "foo", value: 1.0 },
            { name: "bar", value: 10.0 },
            { name: "baz", value: 3.0 },
            ]
    };
    res.json(dummyData);
});

app.get("/data/jyamada", (req, res) => {
    var dummyData = {
        userid: "jyamada",
        username: "James",
        wins: 100,
        losses: 20,
        color: 
            [
            { red:    .5 },
            { green:  .4 },
            { blue:   .5 },
            { alpha:  1 },
            ],
        someArray: 
            [
            { name: "foo", value: 1.5 },
            { name: "bar", value: 2.5 },
            { name: "baz", value: 3.5 },
            ]
    };
    res.json(dummyData);
});
