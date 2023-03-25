namespace CoreSystems.DependencyInjection
{
    using System.Collections.Generic;
    using UnityEngine;
    /// <summary>
    /// Object context means that all objects installed here will be injected based on childres of him including the holder of this component
    /// </summary>
    [DefaultExecutionOrder(-100)]
    public class InjectObjectContext : InjectDependency
    {
        public override object[] GetFromContext()
        {
            List<IInject> components = new List<IInject>();
            GetComponentsInChildren(components);
            foreach (IInject comp in GetComponents<IInject>())
                components.Add(comp);
            return components.ToArray();
        }
    }
}