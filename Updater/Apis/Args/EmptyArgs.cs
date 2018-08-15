namespace Updater.Apis.Args
{
	public class EmptyArgs : IRunnerArgs
	{
		protected EmptyArgs()
		{
		}

		public static IRunnerArgs Create(params object[] parameters)
		{
			return new EmptyArgs();
		}

		public static EmptyArgs Create()
		{
			return (EmptyArgs) Create((object) null);
		}
	}
}