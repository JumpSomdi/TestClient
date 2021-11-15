using System;

namespace TestClient.Comand.BaseComand
{
    public class LambdaCommand : Command
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public LambdaCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentException(nameof(execute));
            _canExecute = canExecute ?? throw new ArgumentException(nameof(canExecute));
        }
        public LambdaCommand(Action<object> execute)
            : this(execute, o => true)
        { }

        public override bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        public override void Execute(object parameter)
        {
            _execute(parameter);
        }

        public static LambdaCommand Create<T>(Action<T> execute, Func<T, bool> canExecute)
        {
            return new LambdaCommand
            (
               execute != null ? new Action<object>(o => { if (o is T t) execute(t); }) : throw new ArgumentException(nameof(execute)),
               canExecute != null ? new Func<object, bool>(o => (o is T t) && canExecute(t)) : throw new ArgumentException(nameof(canExecute))
            );
        }
        public static LambdaCommand Create<T>(Action<T> execute)
        {
            return Create(execute, t => true);
        }
        public static LambdaCommand Create(Action execute, Func<bool> canExecute)
        {
            return new LambdaCommand
            (
               execute != null ? new Action<object>(o => { if (o is null) execute(); }) : throw new ArgumentException(nameof(execute)),
               canExecute != null ? new Func<object, bool>(o => (o is null) && canExecute()) : throw new ArgumentException(nameof(canExecute))
            );
        }
        public static LambdaCommand Create(Action execute)
        {
            return Create(execute, () => true);
        }
    }
}
