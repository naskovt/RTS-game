using UnityEngine;

public static class Custom{


    public static GameObject findChildWithTag(GameObject parent, string searchedTag)
    {
        Component[] children;

        children = parent.GetComponentsInChildren<Component>();

        foreach (var child in children)
        {
            if (child.tag == searchedTag)
            {
                return child.gameObject;
            }
        }

        throw new MissingComponentException(
            "Component in parent: \"" + parent.name + "\" with tag: \"" + searchedTag + "\" not found!");
    }


    public static bool DoesObjectHaveComponent<T>(this GameObject flag) where T : Component
    {
        return flag.GetComponent<T>() != null;
    }
}
