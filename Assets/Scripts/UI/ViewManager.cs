using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    public static ViewManager _instance;

    [SerializeField]
    private View[] views;

    [SerializeField]
    private bool autoInitialize;

    [SerializeField]
    private View defaultView;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        if (autoInitialize)
        {
            Initialize();
        }
    }

    public void Initialize()
    {
        foreach (View view in views)
        {
            view.Initialize();

            view.Hide();
        }

        if (defaultView != null)
        {
            defaultView.Show();
        }
    }

    public static void ChangeView<T>(object args = null) where T : View
    {
        foreach (View view in _instance.views)
        {
            if (view is T)
            {
                view.Show(args);
            }
            else
            {
                view.Hide();
            }
        }
    }
}
