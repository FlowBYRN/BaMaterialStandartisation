public async Task<CostStructureDto> GenerateMaterialCostStructureAsync(ICollection<string> materialNames, bool structurizeFine)
{
    if (materialNames is null)
        throw new ArgumentNullException(nameof(materialNames));

    materialNames = materialNames.Distinct().ToArray();

    List<Material> materials = await mMaterialRepository.CreateNewAsync(materialNames
        .Select(name =>
            new Material()
            {
                MaterialBezeichnung = name,
                Appearances = 1
            }));

    ICollection<MaterialStructureNode> result = mMaterialStructorizer
        .StructurizeMaterials(materials.Select(m => m.PreprocessedMaterialBezeichnung).ToArray(), structurizeFine);

    var dto = new CostStructureDto();
    dto.SetMaterials(result.Select(r => r.ToDto()).ToList());
    return dto;
}