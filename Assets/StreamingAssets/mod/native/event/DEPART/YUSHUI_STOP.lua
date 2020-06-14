trigger = function()
    return  gm_depart().buffers:is_valid('YUSHUI') 
             and gm_depart().buffers:exist_days('YUSHUI') > 30
end

occur_days = function ()
    return 60
end

title = function ()
    return {'YUSHUI_STOP_TITLE', gm_depart().name}
end
desc = function ()
    return {'YUSHUI_STOP_DESC', gm_depart().name}
end

options = 
{
    OPTION_1 = 
    {
        selected = function ()
            gm_depart().buffers:set_invalid('YUSHUI')
        end,

        tooltip = function ()
            return { {'DEPART_BUFFER_INVAILD', gm_depart().name, 'YUSHUI'} }
        end
    }
}