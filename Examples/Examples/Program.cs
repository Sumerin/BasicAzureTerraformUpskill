using Newtonsoft.Json;
using S_RiskAssesment;

var riskAnalyzer = RiskAssessment.Create("BDD3CBE2-4692-4E74-BF19-79444229343B");

await using var file = new FileStream("./1.pdf", FileMode.Open);


var result = await riskAnalyzer.Scan(file);

Console.WriteLine(JsonConvert.SerializeObject(result));