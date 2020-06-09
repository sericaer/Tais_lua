EVENT_DEF.DEPART.EVENT_DEPART_HARVEST_LESS = 
{
    options = 
    {
        OPTION_1 = 
        {
            selected = function ()
                gm_depart().buffers:find('HARVEST_LESS').exist = true
            end,
        }
    }
}