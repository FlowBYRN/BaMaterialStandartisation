PythonEngine.Initialize();

using (Py.GIL())
{
    using (PyModule scope = Py.CreateScope())
    {
        scope.Set("material_raw", materials);
        scope.Exec(File.ReadAllText(GetPythonScript("preprocess")));
        string materialPreprocessed = scope.Get("material_preprocessed").As<string>();
    }
}