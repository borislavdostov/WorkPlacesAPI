namespace Workplaces.DataModel
{
    public class ValidationMessage
    {
        public const string IncorrectDateOfBirth =
            "Date of birth should be lower than today's date.";
        public const string UserNotOldEnough =
            "User is not old enough to work.";
        public const string IncorrectName =
            "Name should start with a capital letter and cannot contain numbers or any special symbols.";
    }
}
