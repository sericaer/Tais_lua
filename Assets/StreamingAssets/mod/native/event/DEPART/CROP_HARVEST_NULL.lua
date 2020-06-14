title = function ()
    return {'CROP_HARVEST_NULL_TITLE', gm_depart().name}
end
desc = function ()
    return {'CROP_HARVEST_NULL_DESC', gm_depart().name}
end

options = 
{
    OPTION_1 = 
    {
        selected = function ()
            gm_depart().buffers:set_valid('HARVEST_NULL')
        end,

        tooltip = function ()
            return { {'DEPART_BUFFER_INVAILD', gm_depart().name, 'HARVEST_NULL'} }
        end
    }
}