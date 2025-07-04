using System.Runtime.CompilerServices;

using Bogus;

namespace CLUK.Skeleton.API.Tests.TestDoubles;

public abstract class FakerForConstructorInitialisedTypes<T> : Faker<T>
    where T : class
{
    /*
     * Override this in your derived class if you have a need to not use StrictMode - do so sparingly.
     */
    protected virtual bool OverrideStrictMode => false;

    protected FakerForConstructorInitialisedTypes()
    {
        StrictMode(OverrideStrictMode is false);

        AllowNotCallingAnyConstructor();
    }

    private void AllowNotCallingAnyConstructor()
    {
        /*
             * Note for this to work, your properties need to be defined as 'init' and any 'RuleFor' calls layered in prior to the Faker<T> being 'generated' to T

             * (either by .Generate() or via Covariance/implicit cast)
        */
        CustomInstantiator(_ => (RuntimeHelpers.GetUninitializedObject(typeof(T)) as T)!);
    }
}