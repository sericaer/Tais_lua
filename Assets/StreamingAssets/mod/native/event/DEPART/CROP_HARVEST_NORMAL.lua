hide = true

title = function ()
    return {'CROP_HARVEST_NORMAL_TITLE', gm_depart().name}
end
desc = function ()
    return {'CROP_HARVEST_NORMAL_DESC', gm_depart().name}
end

options = 
{
    OPTION_1 = 
    {
        selected = function ()
        end,
    }
}