trigger = function()
    return  gm_depart().is_crop_growing == true 
                and gm_depart().buffers:is_valid('YUSHUI') == false
end

occur_days = function ()
    return 4*360
end

title = function ()
    return {'YUSHUI_START_TITLE', gm_depart().name}
end
desc = function ()
    return {'YUSHUI_START_DESC', gm_depart().name}
end

options = 
{
    OPTION_1 = 
    {
        selected = function ()
            gm_depart().buffers:set_valid('YUSHUI')
        end,

        tooltip = function ()
            return { {'DEPART_BUFFER_VAILD', gm_depart().name, 'YUSHUI'} }
        end
    }
}