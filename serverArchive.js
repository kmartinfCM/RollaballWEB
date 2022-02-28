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