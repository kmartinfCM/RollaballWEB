const csv2json = require('./csv2json.js');

//const csv = 'data.csv';

const csv = `profile,username,wins,losses,red,green,blue,alpha,foo,bar,baz
default,defaultUser,18,50,0.7,0.2,0.1,1,0.1,0.15,0.19
kmartinf,Martin,15,70,0.7,0.3,0.6,1,0.3,0.35,0.39
jyamada,James,10,20,0.3,0.7,0.6,1,0.6,0.65,0.69`;



const json = csv2json(csv, {parseNumbers: true});
console.log(json);