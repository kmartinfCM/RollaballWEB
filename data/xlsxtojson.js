var XLSX = require('./node_modules/xlsx-style/xlsx.js');
var data = ReadFile('data.xlsx');

function ReadFile(filename)
{  
  var workbook = XLSX.readFile(filename);
  var name = workbook.SheetNames[0];
  var sheet = workbook.Sheets[name];
  return XLSX.utils.sheet_to_json(sheet);
}

console.log(data);
