# microDI.NET #

Tiny dependency injection container for small applications. 

### What? Another IoC container? ###

Yes, this is another IoC container. It facilitates building small and tiny applications and provides developers with the following advantages:

* Small library without additional dependencies.
* Simplified object creation. Programmer can choose how to create: automatically or customized.
* No special namings or other things to register multiple implementations of same interface: one interface, one implementation.
* Custom life cycle policy defintion.

### How do I get set up? ###

Minimalist setup is to create instance of Container class and that's all.

Basic usage is that:
```cs
// IoC container setup
var container = new Container();

// Register MyClass as transient: on every resolve activator factory will produce new instance
container.Register<MyClass>(new TransientLifeCyclePolicy());

// Resolve instance somewhere
var instance = container.Resolve<MyClass>();
```

There are three life cycle policies to be used:

* Transient policy
* Singleton policy
* Thread-local singleton policy

Usages of these life cycle policies:
```cs
// Singleton life cycle policy example
container.Register<MyClass>(new SingletonLifeCyclePolicy());

// Thread-local singleton life cycle policy example
container.Register<MyOtherClass>(new ThreadLocalLifeCyclePolicy());
```

Interface and implementation registering:
```cs
// Register interface
container.RegisterAs<IMyInterface, MyInterfaceImplementation>(new TransientLifeCyclePolicy());

// Resolve it somewhere
var result = container.Resolve<IMyInterface>();
```

All examples above are using automatic injection of dependency of dependencies it's default behaviour. MicroDI has feature to do customized instance creation via user-provided delegate:
```cs
// Register class via custom initializer
container.Register<MyClass>(
    c => new MyClass(c.Resolve<Dependency1>(), c.Resolve<Dependency2>()),
    new TransientLifeCyclePolicy());

// Register interface via custom initializer
container.RegisterAs<IMyInterface, MyInterfaceImplementation>(
    c => new MyInterfaceImplementation(c.Resolve<Dependency1>(), c.Resolve<Dependency2>()),
    new TransientLifeCyclePolicy());
```

Customizations of life cycle policies is another feature. All implementations must be inherited from *ILifeCyclePolicy* interface. Here is small example of implementation singleton that is used in the library:
```cs
class SingletonLifeCyclePolicy : ILifeCyclePolicy
{
    private object _value;

    public object Get(IActivationFactory activationFactory, RegisteredType registeredType)
    {
        object result;

        if (_value != null)
            result = _value;
        else
            result = _value = activationFactory.GetInstance(registeredType);

        return result;
    }
}
```