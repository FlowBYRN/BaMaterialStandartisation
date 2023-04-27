public static readonly string ModelPreprocess = "preprocess";
public static readonly string InputPreprocess = "material_raw";
public static readonly string OutputPreprocess = "material_preprocessed";

public string PreprocessMaterial(string material)
{
    try
    {
        PythonEngine.Initialize();

        using (Py.GIL())
        {
            using (PyModule scope = Py.CreateScope())
            {
                dynamic preprocessScript = scope.Import(PythonKiExecuterConstants.ModelPreprocess);
                string materialPreprocessed = preprocessScript.preprocess(material);

                return materialPreprocessed;
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        throw;
    }
    finally
    {
        PythonEngine.Shutdown();
    }
}