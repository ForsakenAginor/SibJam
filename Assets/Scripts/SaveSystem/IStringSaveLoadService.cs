namespace Assets.Scripts.SaveSystem
{
    public interface IStringSaveLoadService
    {
        public string GetSavedInfo();

        public void SaveInfo(string value);
    }
}