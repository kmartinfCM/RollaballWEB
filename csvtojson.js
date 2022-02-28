const csv2json = require('./node_modules/csvjson-csv2json/csv2json.js');

const csv = `profile,username,wins,losses,red,green,blue,alpha,foo,bar,baz
default,defaultUser,18,50,0.7,0.2,0.1,1,0.1,0.15,0.19
kmartinf,Martin,15,70,0.7,0.3,0.6,1,0.3,0.35,0.39
jyamada,James,10,20,0.3,0.7,0.6,1,0.6,0.65,0.69`;


//const csv = 'data.csv';
console.log(csv);


const json = csv2json(csv, {parseNumbers: true});
console.log(json);