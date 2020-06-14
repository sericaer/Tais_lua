title = function ()
    return {'CROP_HARVEST_LESS_TITLE', gm_depart().name}
end
desc = function ()
    return {'CROP_HARVEST_LESS_DESC', gm_depart().name}
end

options = 
{
    OPTION_1 = 
    {
        selected = function ()
            gm_depart().buffers:set_valid('HARVEST_LESS')
        end,

        tooltip = function ()
            return { {'DEPART_BUFFER_INVAILD', gm_depart().name, 'HARVEST_LESS'} }
        end
    }
}