public MulticlassClassificationMetrics Train(ICollection<Material> materials)
{
    // Load your data
    var materialsDv = mlContext.Data.LoadFromEnumerable(trainable);

    //Define your training pipeline
    var pipeline =
        mlContext.Transforms.Conversion.MapValueToKey("Label", "MaterialCategory")
            .Append(mlContext.Transforms.Text.FeaturizeText("MaterialBezeichnungFeaturized", options, "MaterialBezeichnung"))
            .AppendCacheCheckpoint(mlContext)
            .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy(featureColumnName: "MaterialBezeichnungFeaturized", labelColumnName: "Label"))
            .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

    // Train and save the model
    var model = pipeline3.Fit(split.TrainSet);
    mlContext.Model.Save(model, materialsDv.Schema, MODEL_PATH);
    //Evaluate the model
    var testdata = model.Transform(split.TestSet);
    var evaluation = mlContext.MulticlassClassification.Evaluate(testdata);
    return evaluation;
}

