namespace GuedesTime.MVC.ViewModels
{
    public class ProgressoCadastroViewModel
    {
        public int EtapasConcluidas { get; set; }
        public List<string> Pendencias { get; set; }
        public string ProximaEtapa { get; set; }
    }

}
