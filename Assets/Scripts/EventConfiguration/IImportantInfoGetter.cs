namespace Assets.Scripts.EventConfiguration
{
    public interface IImportantInfoGetter
    {
        public bool GetImportantStatus(EventNames name);
    }
}