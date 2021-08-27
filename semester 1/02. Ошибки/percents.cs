public static double Calculate(string userInput)
{
	var splittedInput = userInput.Split();
    var depositAmount = double.Parse(splittedInput[0], CultureInfo.InvariantCulture);
    var depositYearlyRate = double.Parse(splittedInput[1], CultureInfo.InvariantCulture);
    var depositTermInMonths = int.Parse(splittedInput[2]);
	return depositAmount * Math.Pow(1 + depositYearlyRate / (12 * 100.0), depositTermInMonths);
}